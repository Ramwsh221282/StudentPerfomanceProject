import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { NgForOf, NgIf } from '@angular/common';
import { AddIconButtonComponent } from '../../../building-blocks/buttons/add-icon-button/add-icon-button.component';
import { GreenOutlineButtonComponent } from '../../../building-blocks/buttons/green-outline-button/green-outline-button.component';
import { EducationDirection } from '../../../modules/administration/submodules/education-directions/models/education-direction-interface';
import { SearchDirectionsService } from './search-directions.service';
import { EducationDirectionItemBlockComponent } from './education-direction-item-block/education-direction-item-block.component';
import { ScrollingModule } from '@angular/cdk/scrolling';
import { CreateEducationDirectionDialogComponent } from './create-education-direction-dialog/create-education-direction-dialog.component';
import { EditEducationDirectionDialogComponent } from './edit-education-direction-dialog/edit-education-direction-dialog.component';
import { DeleteEducationDirectionDialogComponent } from './delete-education-direction-dialog/delete-education-direction-dialog.component';
import { UnauthorizedErrorHandler } from '../../../shared/models/common/401-error-handler/401-error-handler.service';
import { catchError, Observable, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { animate, style, transition, trigger } from '@angular/animations';

@Component({
  selector: 'app-education-directions-inline-list',
  imports: [
    AddIconButtonComponent,
    GreenOutlineButtonComponent,
    EducationDirectionItemBlockComponent,
    NgForOf,
    ScrollingModule,
    CreateEducationDirectionDialogComponent,
    NgIf,
    EditEducationDirectionDialogComponent,
    DeleteEducationDirectionDialogComponent,
  ],
  templateUrl: './education-directions-inline-list.component.html',
  styleUrl: './education-directions-inline-list.component.scss',
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
export class EducationDirectionsInlineListComponent implements OnInit {
  public educationDirections: EducationDirection[] = [];
  @Output() selectedEducationDirection: EventEmitter<EducationDirection> =
    new EventEmitter();
  @Output() educationDirectionRemoved: EventEmitter<void> = new EventEmitter();

  public currentDirection: EducationDirection | null;
  public educationDirectionEditRequest: EducationDirection | null;
  public educationDirectionRemoveRequest: EducationDirection | null;
  public createDialogVisbility: boolean = false;

  constructor(
    private readonly _service: SearchDirectionsService,
    private readonly _handler: UnauthorizedErrorHandler,
  ) {}

  public ngOnInit() {
    this._service
      .getAll()
      .pipe(
        tap((response) => (this.educationDirections = response)),
        catchError((error: HttpErrorResponse) => {
          this._handler.tryHandle(error);
          return new Observable<never>();
        }),
      )
      .subscribe();
  }

  public handleDirectionCreated(educationDirection: EducationDirection): void {
    this.educationDirections.push(educationDirection);
  }

  public handleDirectionRemoved(educationDirection: EducationDirection): void {
    this.educationDirections.splice(
      this.educationDirections.indexOf(educationDirection),
      1,
    );
    if (
      this.currentDirection &&
      this.currentDirection.id == this.currentDirection.id
    ) {
      this.currentDirection = null;
      this.selectedEducationDirection.emit(null!);
    }
  }
}
