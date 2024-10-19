import { Component } from '@angular/core';
import { UserCardComponent } from './user-card/user-card.component';
import { AuthService } from '../../../modules/users/services/auth.service';
import { UserCardOptionsComponent } from './user-card-options/user-card-options.component';

@Component({
  selector: 'app-user-page',
  standalone: true,
  imports: [UserCardComponent, UserCardOptionsComponent],
  templateUrl: './user-page.component.html',
  styleUrl: './user-page.component.scss',
})
export class UserPageComponent {
  public constructor(protected readonly authService: AuthService) {}
}
