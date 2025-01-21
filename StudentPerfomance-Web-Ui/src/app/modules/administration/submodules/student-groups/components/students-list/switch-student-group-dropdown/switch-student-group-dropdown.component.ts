import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Student } from '../../../../students/models/student.interface';
import { StudentGroup } from '../../../services/studentsGroup.interface';
import { UserOperationNotificationService } from '../../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { StudentGroupSearchService } from '../../../services/student-group-search.service';
import { ISubbmittable } from '../../../../../../../shared/models/interfaces/isubbmitable';
import { SwitchStudentGroupService } from './switch-student-group.service';
import { catchError, Observable, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
    selector: 'app-switch-student-group-dropdown',
    templateUrl: './switch-student-group-dropdown.component.html',
    styleUrl: './switch-student-group-dropdown.component.scss',
    standalone: false
})
export class SwitchStudentGroupDropdownComponent
  implements OnInit, ISubbmittable
{
  @Input({ required: true }) student: Student;
  @Input({ required: true }) currentStudentGroup: StudentGroup;
  @Input({ required: true }) visibility: boolean = false;

  @Output() visibilityChange: EventEmitter<boolean> = new EventEmitter();
  @Output() studentMoveToOtherGroup: EventEmitter<Student> =
    new EventEmitter<Student>();
  @Output() otherGroup: EventEmitter<StudentGroup> =
    new EventEmitter<StudentGroup>();

  protected readonly String = String;

  protected chooseOtherGroupLabel = 'Выберите другую группу';
  protected newStudentGroup: StudentGroup | null = null;
  protected isSelectingNewGroup: boolean = false;
  protected groups: StudentGroup[] = [];
  protected groupNames: string[] = [];

  public constructor(
    private readonly _notificationService: UserOperationNotificationService,
    private readonly _groupsSearchService: StudentGroupSearchService,
    private readonly _switchService: SwitchStudentGroupService,
  ) {}

  public ngOnInit() {
    this._groupsSearchService.getAllGroups().subscribe((response) => {
      this.groups = response;
      this.groups = this.excludeCurrentGroupFromList();
      this.groupNames = this.groups.map((group) => group.name);
    });
  }

  public submit() {
    if (this.isNewGroupNotSelected()) return;
    this._switchService
      .switchStudentGroup(
        this.student.id,
        this.currentStudentGroup.id,
        this.newStudentGroup!.id,
      )
      .pipe(
        tap((response) => {
          this.studentMoveToOtherGroup.emit(this.student);
          this.otherGroup.emit(this.newStudentGroup!);
          this.closeDropdown();
        }),
        catchError((error: HttpErrorResponse) => {
          this._notificationService.SetMessage = error.error;
          this._notificationService.failure();
          return new Observable<never>();
        }),
      )
      .subscribe();
  }

  protected closeDropdown(): void {
    this.visibility = false;
    this.visibilityChange.emit(this.visibility);
  }

  protected handleGroupSelection(name: string): void {
    this.newStudentGroup = this.groups.find((group) => group.name == name)!;
    this.chooseOtherGroupLabel = name;
  }

  private isNewGroupNotSelected(): boolean {
    if (this.newStudentGroup == null || this.newStudentGroup.name == null) {
      this._notificationService.SetMessage =
        'Группа в которую нужно перевести студента не выбрана';
      this._notificationService.failure();
      return true;
    }
    return false;
  }

  private excludeCurrentGroupFromList(): StudentGroup[] {
    const groups: StudentGroup[] = [];
    for (const group of this.groups) {
      if (group.name == this.currentStudentGroup.name) continue;
      groups.push(group);
    }
    return groups;
  }
}
