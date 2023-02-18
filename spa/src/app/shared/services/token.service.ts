import { Injectable } from '@angular/core';

const ACCESS_TOKEN = 'access_token';
const REFRESH_TOKEN = 'refresh_token';

@Injectable({
  providedIn: 'root'
})
export class TokenService {

  constructor() { }

  getToken(): string {
    return localStorage.getItem(ACCESS_TOKEN);
  }

  getRefreshToken(): string {
    return localStorage.getItem(REFRESH_TOKEN);
  }

  saveToken(token): void {
    localStorage.setItem(ACCESS_TOKEN, token);
  }

  saveRefreshToken(refreshToken): void {
    localStorage.setItem(REFRESH_TOKEN, refreshToken);
  }

  removeToken(): void {
    localStorage.removeItem(ACCESS_TOKEN);
  }

  removeRefreshToken(): void {
    localStorage.removeItem(REFRESH_TOKEN);
  }

  getRoles(): Array<string> {
    let user = this.getUser();
    if (Array.isArray(user.role)) {
      return user.role;
    }
    return [user.role];
  }

  getUser() {
    let token = localStorage.getItem(ACCESS_TOKEN);
    let data = token.split('.')[1]
    let decoded = window.atob(data);
    let parsed = JSON.parse(decoded);
    return parsed;
  }
}
