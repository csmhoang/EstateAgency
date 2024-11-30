import { CommonModule } from '@angular/common';
import { Component, DestroyRef, inject, Input, OnInit } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import {
  AbstractControl,
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { provideNativeDateAdapter } from '@angular/material/core';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { CartDetail } from '@features/Cart/models/cart-detail.model';
import { CartService } from '@features/Cart/services/cart.service';
import { Post } from '@features/post/models/post.model';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { catchError, of } from 'rxjs';

@Component({
  selector: 'app-cart-append',
  standalone: true,
  providers: [provideNativeDateAdapter()],

  imports: [
    MatFormFieldModule,
    MatInputModule,
    MatDatepickerModule,
    ReactiveFormsModule,
    MatSelectModule,
    CommonModule,
  ],
  templateUrl: './cart-append.component.html',
  styleUrl: './cart-append.component.scss',
})
export class CartAppendComponent implements OnInit {
  @Input() data!: Post;
  cart = this.cartService.currentCart();

  destroyRef = inject(DestroyRef);
  activeModal = inject(NgbActiveModal);

  form: FormGroup = new FormGroup({});
  numberOfMonth?: AbstractControl | null;
  numberOfTenant?: AbstractControl | null;

  constructor(
    private formBuilder: FormBuilder,
    private cartService: CartService
  ) {}

  ngOnInit() {
    this.form = this.formBuilder.group({
      numberOfMonth: this.formBuilder.control('', [Validators.required]),
      numberOfTenant: this.formBuilder.control('', [Validators.required]),
      note: this.formBuilder.control(''),
    });

    this.numberOfMonth = this.form.get('numberOfMonth');
    this.numberOfTenant = this.form.get('numberOfTenant');
  }

  onAppend() {
    if (this.form.valid && this.cart) {
      const price = this.data.room!.price;
      const cartDetail: CartDetail = {
        ...this.form.value,
        cartId: this.cart?.id,
        roomId: this.data.roomId,
        price: price * this.numberOfMonth?.value,
      };
      this.cartService
        .append(cartDetail)
        .pipe(
          takeUntilDestroyed(this.destroyRef),
          catchError(() => of(null))
        )
        .subscribe((response) => {
          if (response?.success) {
            this.activeModal.close(true);
          }
        });
    }
  }

  accept() {
    this.onAppend();
  }

  decline() {
    this.activeModal.dismiss(false);
  }

  errorForNumberOfTenant(): string {
    if (this.numberOfTenant?.hasError('required')) {
      return 'Số người ở không được để trống!';
    }
    return '';
  }

  errorFornumberOfMonth(): string {
    if (this.numberOfMonth?.hasError('required')) {
      return 'Số tháng thuê không được để trống!';
    }
    return '';
  }
}
