import { Component, EventEmitter, Input, Output } from '@angular/core';
import { UserRecord } from '../../../modules/administration/submodules/users/services/user-table-element-interface';
import { AddIconButtonComponent } from '../../../building-blocks/buttons/add-icon-button/add-icon-button.component';
import { GreenOutlineButtonComponent } from '../../../building-blocks/buttons/green-outline-button/green-outline-button.component';
import { NgForOf, NgIf } from '@angular/common';
import { UsersListItemComponent } from './users-list-item/users-list-item.component';
import { animate, style, transition, trigger } from '@angular/animations';

@Component({
  selector: 'app-users-management-list',
  imports: [
    AddIconButtonComponent,
    GreenOutlineButtonComponent,
    NgIf,
    UsersListItemComponent,
    NgForOf,
  ],
  templateUrl: './users-management-list.component.html',
  styleUrl: './users-management-list.component.scss',
  animations: [
    trigger('fadeIn', [
      transition(':enter', [
        style({ opacity: 0, transform: 'translateY(-10px)' }),
        animate(
          '300ms ease-out',
          style({ opacity: 1, transform: 'translateY(0)' }),
        ),
      ]),
      transition(':leave', [
        animate(
          '300ms ease-in',
          style({ opacity: 0, transform: 'translateY(-10px)' }),
        ),
      ]),
    ]),
  ],
})
export class UsersManagementListComponent {
  @Input({ required: true }) users: UserRecord[] = [];
  @Output() createUserClicked: EventEmitter<void> = new EventEmitter();
  @Output() removeUserRequested: EventEmitter<UserRecord> = new EventEmitter();
  @Output() editUserRequested: EventEmitter<UserRecord> = new EventEmitter();
}
