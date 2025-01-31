import { Component, EventEmitter, Input, Output } from '@angular/core';
import { UserRecord } from '../../../../modules/administration/submodules/users/services/user-table-element-interface';
import { EditIconButtonComponent } from '../../../../building-blocks/buttons/edit-icon-button/edit-icon-button.component';
import { RemoveIconButtonComponent } from '../../../../building-blocks/buttons/remove-icon-button/remove-icon-button.component';
import { NgClass, NgOptimizedImage } from '@angular/common';
import { UsersPageViewmodel } from '../../users-page.viewmodel';

@Component({
  selector: 'app-users-list-item',
  imports: [
    EditIconButtonComponent,
    RemoveIconButtonComponent,
    NgClass,
    NgOptimizedImage,
  ],
  templateUrl: './users-list-item.component.html',
  styleUrl: './users-list-item.component.scss',
  standalone: true,
})
export class UsersListItemComponent {
  @Input({ required: true }) user: UserRecord;
  @Output() selectForRemove: EventEmitter<UserRecord> = new EventEmitter();
  @Output() selectForEdit: EventEmitter<UserRecord> = new EventEmitter();

  public constructor(protected readonly viewModel: UsersPageViewmodel) {}

  public select($event: MouseEvent): void {
    $event.stopPropagation();
    this.viewModel.select(this.user);
  }
}
