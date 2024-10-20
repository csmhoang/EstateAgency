import { HttpContextToken } from '@angular/common/http';

export const SkipPreloader = new HttpContextToken(() => false);

export const SkipApi = new HttpContextToken(() => false);



