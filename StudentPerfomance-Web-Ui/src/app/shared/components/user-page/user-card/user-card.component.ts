import { Component, Input } from '@angular/core';
import { User } from '../../../../modules/users/services/user-interface';

@Component({
  selector: 'app-user-card',
  standalone: true,
  imports: [],
  templateUrl: './user-card.component.html',
  styleUrl: './user-card.component.scss',
})
export class UserCardComponent {
  @Input({ required: true }) user: User;
}
