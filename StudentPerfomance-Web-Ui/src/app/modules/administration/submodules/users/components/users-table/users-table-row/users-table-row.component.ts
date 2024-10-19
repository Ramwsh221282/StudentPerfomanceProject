import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { UserRecord } from '../../../services/user-table-element-interface';
import { AuthService } from '../../../../../../users/services/auth.service';

@Component({
  selector: 'app-users-table-row',
  templateUrl: './users-table-row.component.html',
  styleUrl: './users-table-row.component.scss',
})
export class UsersTableRowComponent implements OnInit {
  @Input({ required: true }) user: UserRecord;
  @Output() deletionEmitter: EventEmitter<void> = new EventEmitter();

  protected isYou: boolean;

  public constructor(private readonly _authService: AuthService) {}

  public ngOnInit(): void {
    this.isYou = this._authService.userData.email == this.user.email;
  }
}
