import { Component, OnInit } from '@angular/core';
import { ErrorStateMatcher } from '@angular/material/core';
import { FormBuilder, FormControl, FormGroup, FormGroupDirective, NgForm, Validators } from '@angular/forms';
import { AuthService } from './../../shared/services/auth.service';
import {ActivatedRoute, Data, Router} from '@angular/router';
import { CommonErrorStateMatcher } from "../../shared/common/common.error-state-matcher";
import { ConfirmPasswordValidator } from '../../shared/common/confirm-password.validator';
import {TokenService} from "../../shared/services/token.service";
import {AuthRedirectionModel} from "../../shared/models/auth-redirection.model";
import {ToastrService} from "ngx-toastr";

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  registerForm: FormGroup;
  username = '';
  password = '';
  name = '';
  isLoadingResults = false;
  matcher = new CommonErrorStateMatcher();

  constructor(private authService: AuthService,
              private router: Router,
              private formBuilder: FormBuilder,
              private tokenService: TokenService,
              private activatedRoute: ActivatedRoute,
              private toastr: ToastrService) { }

  ngOnInit(): void {
    // if (this.isLoggedIn()) {
    //   this.redirectLoggedIn();
    // }
    this.registerForm = this.formBuilder.group({
      username : [null, Validators.required],
      password : [null, Validators.required],
      confirmPassword : [null],
      displayName : [null, Validators.required]
    },{
      validators: ConfirmPasswordValidator('password', 'confirmPassword'),
    });
  }

  onFormSubmit(): void {
    this.isLoadingResults = true;
    this.authService.register(this.registerForm.value)
      .subscribe((res: any) => {
        this.isLoadingResults = false;
        this.router.navigate(['/login']).then(_ => console.log('You are registered now!'));
      }, (err: any) => {
        console.log(err);
        this.toastr.error(err);
        this.isLoadingResults = false;
      });
  }

  public isLoggedIn() {
    return this.tokenService.getRefreshToken()
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
