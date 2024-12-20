import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { UserRecord } from '../../../services/user-table-element-interface';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-users-item',
  templateUrl: './users-item.component.html',
  styleUrl: './users-item.component.scss',
})
export class UsersItemComponent implements OnInit {
  @Input({ required: true }) user: UserRecord;
  @Input({ required: true }) isCurrentlySelected: boolean = false;
  @Output() selectUser: EventEmitter<UserRecord> = new EventEmitter();
  @Output() requestRemove: EventEmitter<UserRecord> = new EventEmitter();

  public constructor(private readonly _datePipe: DatePipe) {}

  public ngOnInit(): void {
    this.user.lastTimeOnline = this._datePipe.transform(
      this.user.lastTimeOnline,
      'dd-MM-yyyy',
    );
  }
}
