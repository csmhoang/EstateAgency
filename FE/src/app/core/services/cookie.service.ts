import { Injectable } from '@angular/core';
import { Secret } from '@core/models/secret.model';

@Injectable({
  providedIn: 'root',
})
export class CookieService {
  private cookieStore: { [key: string]: any } = {};

  get(key: string) {
    const cookies = document.cookie;
    if (!!cookies === false) {
      return;
    }
    const cookieArr = cookies.split(';');
    for (let i = 0; i < cookieArr.length; i++) {
      const start = cookieArr[i].indexOf('=');
      const key = cookieArr[i].substring(0, start);
      const value = cookieArr[i].substring(start + 1);
      this.cookieStore[key.trim()] = value.trim();
    }
    return !!this.cookieStore[key] ? this.cookieStore[key] : null;
  }

  remove() {
    document.cookie =
      'token=;expires=Thu, 01 Jan 1970 00 :00:00 UTC;path=/; Secure';
    document.cookie =
      'refreshToken=;expires=Thu, 01 Jan 1970 00 :00:00 UTC;path=/; Secure';
    document.cookie =
      'email=;expires=Thu, 01 Jan 1970 00 :00:00 UTC;path=/; Secure';
    document.cookie =
      'password=;expires=Thu, 01 Jan 1970 00 :00:00 UTC;path=/; Secure';
    document.cookie =
      'isRemember=;expires=Thu, 01 Jan 1970 00 :00:00 UTC;path=/; Secure';
  }

  save(secret: Secret, expDays: number) {
    const date = new Date();
    date.setTime(date.getTime() + expDays * 24 * 60 * 60 * 1000);
    document.cookie = `token=${
      secret.accessToken
    }; expires=${date.toUTCString()}; path=/; Secure;`;
    document.cookie = `refreshToken=${
      secret.refreshToken
    }; expires=${date.toUTCString()}; path=/; Secure;`;
    document.cookie = `email=${
      secret.email
    }; expires=${date.toUTCString()}; path=/; Secure;`;
    document.cookie = `password=${
      secret.password
    }; expires=${date.toUTCString()}; path=/; Secure;`;
    document.cookie = `isRemember=${
      secret.isRemember
    }; expires=${date.toUTCString()}; path=/; Secure;`;
  }
}
