import { Component } from '@angular/core';
import {environment} from "../../environments/environment";
import {TokenService} from "../shared/services/token.service";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = environment.appName;
  constructor(private tokenService: TokenService) {
  }

  public isAuthenticated() {
    return !!this.tokenService.getRefreshToken();
  }
}
