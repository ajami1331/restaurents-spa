import { Injectable } from '@angular/core';
import {CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router} from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from "../shared/services/auth.service";
import { TokenService } from "../shared/services/token.service";
import {AuthRedirectionModel} from "../shared/models/auth-redirection.model";

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private authService: AuthService, private tokenService: TokenService, private router: Router) {
  }
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    const url: string = state.url;
    const roles = route.data['roles'] as Array<string>;
    const authRedirections: AuthRedirectionModel[] = route.data['authRedirections'];
    if (!roles) {
      return this.checkIsUserAuthenticated(url);
    }
    return this.checkIfRoleMatches(url, roles, authRedirections);
  }

  private checkIsUserAuthenticated(url: string): boolean {
    if (!this.tokenService.getRefreshToken()) {
      this.redirectUnauthenticated(url);
    }

    return true;
  }

  private checkIfRoleMatches(url: string, roles: Array<string>, authRedirections: AuthRedirectionModel[]) {
    if (!this.tokenService.getRefreshToken()) {
      this.redirectUnauthenticated(url);
    }

    let tokenRoles: string[] = this.tokenService.getRoles();
    if (!tokenRoles.some(tr => roles.includes(tr))) {
      this.redirectUnauthorized(url, authRedirections, tokenRoles);
    }

    return true;
  }

  private redirectUnauthenticated(url: string) {
    this.authService.redirectUrl = url;
    this.router.navigate(['/login']).then(_ => false);
  }

  private redirectUnauthorized(url: string, authRedirections: AuthRedirectionModel[], tokenRoles: string[]) {
    this.authService.redirectUrl = url;
    let redirectUrl = '/login';
    for (let i = 0; i < authRedirections.length; i++) {
      if (tokenRoles.includes(authRedirections[i].Role)) {
        this.authService.redirectUrl = authRedirections[i].Authenticated;
        redirectUrl = authRedirections[i].Unauthenticated;
      }
    }
    this.router.navigate([redirectUrl]).then(_ => false);
  }
}
