import { Component, Input } from '@angular/core';
import { UserRecord } from '../../../modules/administration/submodules/users/services/user-table-element-interface';
import { DatePipe, NgIf } from '@angular/common';

@Component({
  selector: 'app-users-management-user-form',
  imports: [NgIf, DatePipe],
  templateUrl: './users-management-user-form.component.html',
  styleUrl: './users-management-user-form.component.scss',
  standalone: true,
})
export class UsersManagementUserFormComponent {
  @Input({ required: true }) user: UserRecord;
}
