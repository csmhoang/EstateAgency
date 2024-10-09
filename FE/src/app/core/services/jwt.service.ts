import { Injectable } from '@angular/core';
import { Token } from '@core/models/token.model';

@Injectable({
  providedIn: 'root',
})
export class JwtService {
  private cookieStore: { [key: string]: any } = {};

  get(key: string) {
    const cookies = document.cookie;
    if (!!cookies === false) {
      return;
    }
    const cookieArr = cookies.split(';');
    for (let i = 0; i <= cookieArr.length; i++) {
      const prop = cookieArr[i].split('=');
      this.cookieStore[prop[0].trim()] = prop[1].trim();
    }
    return !!this.cookieStore[key] ? this.cookieStore[key] : null;
  }

  remove() {
    document.cookie =
      'token=;refreshToken=;expires=Thu, 01 Jan 1970 00:00:00 UTC;path=/';
  }

  save(secret: Token, expDays: number) {
    const date = new Date();
    date.setTime(date.getTime() + expDays * 24 * 60 * 60 * 1000);
    document.cookie = `token=${secret.token};
      refreshToken=${secret.refreshToken};
      expires=${date.toUTCString()};path=/`;
  }
}
