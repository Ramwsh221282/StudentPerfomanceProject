import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ISuccessNotificatable } from '../../../../../../../shared/models/interfaces/isuccess-notificatable';
import { IFailureNotificatable } from '../../../../../../../shared/models/interfaces/ifailure-notificatable';
import { ISubbmittable } from '../../../../../../../shared/models/interfaces/isubbmitable';
import { StudentGroup } from '../../../services/studentsGroup.interface';

@Component({
  selector: 'app-merge-group-modal',
  templateUrl: './merge-group-modal.component.html',
  styleUrl: './merge-group-modal.component.scss',
})
export class MergeGroupModalComponent
  implements
    OnInit,
    ISuccessNotificatable,
    IFailureNotificatable,
    ISubbmittable
{
  @Input({ required: true }) initial: StudentGroup;
  @Output() visibility: EventEmitter<boolean> = new EventEmitter<boolean>();

  public submit(): void {}

  public notifyFailure(): void {}

  public manageFailure(value: boolean) {}

  public manageSuccess(): void {}

  public ngOnInit(): void {
    throw new Error('Method not implemented.');
  }
}
