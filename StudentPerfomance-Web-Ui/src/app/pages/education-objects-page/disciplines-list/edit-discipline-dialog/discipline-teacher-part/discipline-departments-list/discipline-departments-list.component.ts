import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Department } from '../../../../../../modules/administration/submodules/departments/models/departments.interface';
import { TeachersDepartmentsDataService } from './teachers-departments-data.service';
import { NgForOf, NgIf } from '@angular/common';
import { UnauthorizedErrorHandler } from '../../../../../../shared/models/common/401-error-handler/401-error-handler.service';
import { catchError, Observable, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { animate, style, transition, trigger } from '@angular/animations';

@Component({
  selector: 'app-discipline-departments-list',
  imports: [NgForOf, NgIf],
  templateUrl: './discipline-departments-list.component.html',
  styleUrl: './discipline-departments-list.component.scss',
  standalone: true,
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
export class DisciplineDepartmentsListComponent implements OnInit {
  public departments: Department[] = [];
  @Output() departmentSelected: EventEmitter<Department> = new EventEmitter();

  public constructor(
    private readonly _dataService: TeachersDepartmentsDataService,
    private readonly _handler: UnauthorizedErrorHandler,
  ) {}

  public ngOnInit() {
    this._dataService
      .getDepartments()
      .pipe(
        tap((response) => {
          this.departments = response;
        }),
        catchError((error: HttpErrorResponse) => {
          this._handler.tryHandle(error);
          return new Observable<never>();
        }),
      )
      .subscribe();
  }

  public selectDepartment(department: Department, $event: MouseEvent): void {
    $event.stopPropagation();
    this.departmentSelected.emit(department);
  }
}
