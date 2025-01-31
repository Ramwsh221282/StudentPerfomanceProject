import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import {
  AdminAccessResponse,
  AdminAssignmentsAccessService,
} from './admin-assignments-access.service';
import { catchError, Observable, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { NgForOf, NgIf } from '@angular/common';
import { Department } from '../../../../modules/administration/submodules/departments/models/departments.interface';
import { Teacher } from '../../../../modules/administration/submodules/teachers/models/teacher.interface';
import { YellowButtonComponent } from '../../../../building-blocks/buttons/yellow-button/yellow-button.component';
import { GreenOutlineButtonComponent } from '../../../../building-blocks/buttons/green-outline-button/green-outline-button.component';
import { RedOutlineButtonComponent } from '../../../../building-blocks/buttons/red-outline-button/red-outline-button.component';
import { TeachersDepartmentsDataService } from '../../../education-objects-page/disciplines-list/edit-discipline-dialog/discipline-teacher-part/discipline-departments-list/teachers-departments-data.service';

@Component({
  selector: 'app-admin-assignments-access-resolver-dialog',
  templateUrl: './admin-assignments-access-resolver-dialog.component.html',
  styleUrl: './admin-assignments-access-resolver-dialog.component.scss',
  standalone: true,
  imports: [
    YellowButtonComponent,
    NgForOf,
    NgIf,
    GreenOutlineButtonComponent,
    RedOutlineButtonComponent,
    YellowButtonComponent,
    GreenOutlineButtonComponent,
    RedOutlineButtonComponent,
  ],
})
export class AdminAssignmentsAccessResolverDialogComponent implements OnInit {
  @Input({ required: true }) isAdminAccess: boolean = false;
  @Output() accessGiven: EventEmitter<AdminAccessResponse> = new EventEmitter();
  @Output() visibilityChange: EventEmitter<boolean> = new EventEmitter();
  public departments: Department[] = [];
  public teacher: Teacher | null = null;
  public isDescriptionRead: boolean = false;
  public isSelectingDepartmentStep: boolean = false;
  public isSelectingTeacherStep: boolean = false;
  public isConfirmationStep: boolean = false;
  public selectedDepartment: Department | null = null;
  public selectedTeacher: Teacher | null = null;
  public errorText: string = '';
  public successText: string = '';

  constructor(
    private readonly _departmentsService: TeachersDepartmentsDataService,
    private readonly _adminAccessService: AdminAssignmentsAccessService,
  ) {}

  public ngOnInit() {
    this._departmentsService.getDepartments().subscribe((response) => {
      this.departments = response;
    });
  }

  public resolveDepartment(event: Event, department: Department): void {
    event.stopPropagation();
    this.selectedDepartment = department;
  }

  public resolveTeacher(event: Event, teacher: Teacher): void {
    event.stopPropagation();
    this.selectedTeacher = teacher;
  }

  public tryGetAccess(): void {
    if (!this.selectedTeacher) return;
    this._adminAccessService
      .tryGetAccess(this.selectedTeacher.id)
      .pipe(
        tap((response) => {
          this.successText = 'Доступ предоставлен';
          this.accessGiven.emit(response);
          this.closeWindow();
        }),
        catchError((error: HttpErrorResponse) => {
          this.errorText = error.error;
          return new Observable<never>();
        }),
      )
      .subscribe();
  }

  public closeWindow(): void {
    this.visibilityChange.emit(false);
  }
}
