import { CommonModule } from '@angular/common';
import {
  Component,
  DestroyRef,
  inject,
  Input,
  OnDestroy,
  OnInit,
  signal,
} from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { CartDetail } from '@features/Cart/models/cart-detail.model';
import { CartService } from '@features/Cart/services/cart.service';
import { NgbCarouselModule } from '@ng-bootstrap/ng-bootstrap';
import { catchError, of } from 'rxjs';

@Component({
  selector: 'app-cart-item',
  standalone: true,
  imports: [NgbCarouselModule, CommonModule, ReactiveFormsModule],
  templateUrl: './cart-item.component.html',
  styleUrl: './cart-item.component.scss',
})
export class CartItemComponent implements OnInit, OnDestroy {
  @Input() cartDetail?: CartDetail;
  destroyRef = inject(DestroyRef);

  form: FormGroup = new FormGroup({});
  price = signal<number | undefined>(undefined);

  constructor(
    private formBuilder: FormBuilder,
    private cartService: CartService
  ) {}

  ngOnInit() {
    this.form = this.formBuilder.group({
      numberOfMonth: this.formBuilder.control(this.cartDetail?.numberOfMonth, [
        Validators.required,
      ]),
      numberOfTenant: this.formBuilder.control(
        this.cartDetail?.numberOfTenant,
        [Validators.required]
      ),
    });

    this.price.set(this.cartDetail?.price);
    this.form.get('numberOfMonth')?.valueChanges.subscribe((numberOfMonth) => {
      const room = this.cartDetail?.room;
      if (room) {
        this.price.set(room.price * numberOfMonth);
      }
    });
  }

  ngOnDestroy() {
    if (this.form.valid && this.form.dirty && !this.form.pristine) {
      this.cartService.isCartChanges.set(true);
      this.cartService.currentCart.update((cart) => {
        if (cart?.cartDetails) {
          for (let i = 0; i < cart?.cartDetails.length; i++) {
            if (cart.cartDetails[i].id === this.cartDetail?.id) {
              const room = cart.cartDetails[i].room;
              if (room) {
                const price = room.price;
                const numberOfMonth = this.form.get('numberOfMonth')?.value;
                cart.cartDetails[i].numberOfMonth = numberOfMonth;
                cart.cartDetails[i].numberOfTenant =
                  this.form.get('numberOfTenant')?.value;
                cart.cartDetails[i].price = price * numberOfMonth;
              }
            }
          }
        }
        return cart;
      });
    }
  }

  onRemove() {
    if (this.cartDetail) {
      this.cartService
        .remove(this.cartDetail.id)
        .pipe(
          takeUntilDestroyed(this.destroyRef),
          catchError(() => of(null))
        )
        .subscribe((response) => {
          if (response?.success) {
            this.cartService.currentCart.update((cart) => {
              if (cart) {
                return {
                  ...cart,
                  cartDetails: cart.cartDetails?.filter(
                    (cartDetail) => cartDetail.id !== this.cartDetail!.id
                  ),
                };
              }
              return null;
            });
          }
        });
    }
  }
}
