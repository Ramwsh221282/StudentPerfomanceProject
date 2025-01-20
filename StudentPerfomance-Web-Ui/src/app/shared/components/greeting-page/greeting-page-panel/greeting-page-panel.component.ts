import { Component } from '@angular/core';
import { BlueOutlineButtonComponent } from '../../../../building-blocks/buttons/blue-outline-button/blue-outline-button.component';
import { GreenOutlineButtonComponent } from '../../../../building-blocks/buttons/green-outline-button/green-outline-button.component';
import { Router } from '@angular/router';
import { AuthService } from '../../../../modules/users/services/auth.service';

@Component({
  selector: 'app-greeting-page-panel',
  standalone: true,
  imports: [BlueOutlineButtonComponent, GreenOutlineButtonComponent],
  templateUrl: './greeting-page-panel.component.html',
  styleUrl: './greeting-page-panel.component.scss',
})
export class GreetingPagePanelComponent {
  public constructor(
    private readonly _router: Router,
    private readonly _authService: AuthService,
  ) {}

  public openVkPage(): void {
    const url: string = 'https://vk.com/lfsibgu';
    window.open(url, '_blank');
  }

  public openAuthPage(): void {
    if (this._authService.isAuthorized) {
      this._router.navigate(['/user']);
      return;
    }
    this._router.navigate(['/login']);
  }

  public openMainSibguPage(): void {
    const url: string = 'https://www.lfsibgu.ru/';
    window.open(url, '_blank');
  }
}
