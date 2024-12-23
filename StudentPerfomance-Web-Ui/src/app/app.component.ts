import {
  AfterViewInit,
  APP_INITIALIZER,
  Component,
  OnInit,
} from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { SideBarComponent } from './shared/components/side-bar/side-bar.component';
import { SuccessResultNotificationComponent } from './shared/components/success-result-notification/success-result-notification.component';
import { FailureResultNotificationComponent } from './shared/components/failure-result-notification/failure-result-notification.component';
import { AppConfigService } from './app.config.service';
import { AuthService } from './modules/users/services/auth.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet,
    SideBarComponent,
    RouterLink,
    SuccessResultNotificationComponent,
    FailureResultNotificationComponent,
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
  providers: [
    {
      provide: APP_INITIALIZER,
      multi: true,
      deps: [AppConfigService],
      useFactory: (appConfigService: AppConfigService) => {
        return () => {
          return appConfigService.loadAppConfig();
        };
      },
    },
  ],
})
export class AppComponent implements AfterViewInit, OnInit {
  public constructor(
    private readonly auth: AuthService,
    private readonly _configService: AppConfigService,
  ) {}

  async ngOnInit(): Promise<void> {
    const result = await this._configService.loadAppConfig();
    this._configService.initializeBaseApiUri();
    return result;
  }

  public ngAfterViewInit(): void {
    this.auth.tryAuthorizeUsingCookie();
  }
}
