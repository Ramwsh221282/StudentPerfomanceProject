import {
  AfterViewInit,
  APP_INITIALIZER,
  Component,
  OnInit,
} from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { SideBarComponent } from './shared/components/side-bar/side-bar.component';
import { AppConfigService } from './app.config.service';
import { AuthService } from './pages/user-page/services/auth.service';
import { NgClass } from '@angular/common';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, SideBarComponent, NgClass],
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
  public isMenuHidden: boolean = false;

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
