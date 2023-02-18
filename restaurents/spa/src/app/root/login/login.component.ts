import {Component, OnDestroy, OnInit} from '@angular/core';
import { AuthService } from './../../shared/services/auth.service';
import { FormBuilder, FormControl, FormGroup, FormGroupDirective, NgForm, Validators } from '@angular/forms';
import {ActivatedRoute, Data, Router} from '@angular/router';
import { CommonErrorStateMatcher } from "../../shared/common/common.error-state-matcher";
import {Subscription} from "rxjs";
import {AuthRedirectionModel} from "../../shared/models/auth-redirection.model";
import {TokenService} from "../../shared/services/token.service";
import {map, tap} from "rxjs/operators";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit, OnDestroy {
  loginForm: FormGroup;
  username = '';
  password = '';
  isLoadingResults = false;
  matcher = new CommonErrorStateMatcher();
  private loginSubscription: Subscription;

  constructor(private authService: AuthService,
              private router: Router,
              private formBuilder: FormBuilder,
              private activatedRoute: ActivatedRoute,
              private tokenService: TokenService) { }

  ngOnInit(): void {

    if (this.isLoggedIn()) {
      this.redirectLoggedIn();
    }
    this.loginForm = this.formBuilder.group({
      username : [null, Validators.required],
      password : [null, Validators.required]
    });
  }

  onFormSubmit(): void {
    this.isLoadingResults = true;
    this.loginSubscription = this.authService.login(this.loginForm.value)
      .subscribe(() => {
        this.isLoadingResults = false;
        this.redirectLoggedIn();
      }, (err: any) => {
        console.log(err);
        this.isLoadingResults = false;
      });
  }

  public isLoggedIn() {
    return this.tokenService.getRefreshToken()
  }

  ngOnDestroy(): void {
  }

  private redirectLoggedIn() {
    const data: Data = this.activatedRoute.snapshot.data;
    console.log(data);
    const authRedirections: AuthRedirectionModel[] = data['authRedirections'];
    let tokenRoles: string[] = this.tokenService.getRoles();
    this.authService.redirectUrl = '/login';
    let redirectUrl = '/secure';
    console.log(authRedirections);
    for (let i = 0; i < authRedirections.length; i++) {
      if (tokenRoles.includes(authRedirections[i].Role)) {
        this.authService.redirectUrl = authRedirections[i].Unauthenticated;
        redirectUrl = authRedirections[i].Authenticated;
      }
    }
    this.router.navigate([redirectUrl]).then(_ => false);
  }
}
