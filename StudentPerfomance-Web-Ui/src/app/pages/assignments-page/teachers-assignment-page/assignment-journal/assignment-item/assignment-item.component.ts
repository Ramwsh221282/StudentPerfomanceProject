import { Component, Input, OnInit } from '@angular/core';
import { TeacherJournalStudent } from '../../../models/teacher-journal-students';
import { NgClass, NgForOf, NgIf, NgOptimizedImage } from '@angular/common';
import { animate, style, transition, trigger } from '@angular/animations';
import { NotificationService } from '../../../../../building-blocks/notifications/notification.service';
import { UnauthorizedErrorHandler } from '../../../../../shared/models/common/401-error-handler/401-error-handler.service';
import { AssignmentService } from './assignment.service';
import { catchError, Observable, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { TeacherAssignmentPageViewmodel } from '../../teacher-assignment-page.viewmodel';

@Component({
  selector: 'app-assignment-item',
  imports: [NgOptimizedImage, NgIf, NgForOf, NgClass],
  templateUrl: './assignment-item.component.html',
  styleUrl: './assignment-item.component.scss',
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
export class AssignmentItemComponent implements OnInit {
  @Input({ required: true }) student: TeacherJournalStudent;
  public markName: string = '';
  public isMarksShown: boolean = false;

  public constructor(
    private readonly _notifications: NotificationService,
    private readonly _handler: UnauthorizedErrorHandler,
    private readonly _service: AssignmentService,
    protected readonly viewModel: TeacherAssignmentPageViewmodel,
  ) {}

  private _marks: Mark[] = [
    { value: 0, name: 'НА' } as Mark,
    { value: 1, name: 'X' } as Mark,
    { value: 2, name: '2' } as Mark,
    { value: 3, name: '3' } as Mark,
    { value: 4, name: '4' } as Mark,
    { value: 5, name: '5' } as Mark,
  ];

  public isMarkX(): boolean {
    return this.markName === 'X';
  }

  protected marks: Mark[] = [
    { value: 0, name: 'НА' } as Mark,
    { value: 2, name: '2' } as Mark,
    { value: 3, name: '3' } as Mark,
    { value: 4, name: '4' } as Mark,
    { value: 5, name: '5' } as Mark,
  ];

  public ngOnInit() {
    this.markName = this.resolveMarkName();
  }

  public showMarks($event: MouseEvent): void {
    if (this.viewModel.isLocked) return;
    $event.stopPropagation();
    this.isMarksShown = !this.isMarksShown;
  }

  public resolveMarkName(): string {
    const mark = this._marks.find(
      (mark) => mark.value == this.student.assignment.value,
    )!;
    return mark?.name;
  }

  public selectMark($event: MouseEvent, mark: Mark): void {
    $event.stopPropagation();
    this.student.assignment.value = mark.value;
    this._service
      .makeAssignment(this.student)
      .pipe(
        tap(() => {
          this._notifications.bulkSuccess('Оценка проставлена');
          this.markName = this.resolveMarkName();
          this.isMarksShown = !this.isMarksShown;
        }),
        catchError((error: HttpErrorResponse) => {
          this._handler.tryHandle(error);
          return new Observable<never>();
        }),
      )
      .subscribe();
  }
}

interface Mark {
  value: number;
  name: string;
}
