import { Component, OnInit } from '@angular/core';
import { StudentGroup } from '../../../modules/administration/submodules/student-groups/services/studentsGroup.interface';
import { StudentGroupsDataService } from './student-groups-data.service';
import { AddIconButtonComponent } from '../../../building-blocks/buttons/add-icon-button/add-icon-button.component';
import { GreenOutlineButtonComponent } from '../../../building-blocks/buttons/green-outline-button/green-outline-button.component';
import { StudentGroupCardComponent } from './student-group-card/student-group-card.component';
import { NgForOf, NgIf } from '@angular/common';
import { CreateGroupDialogComponent } from './create-group-dialog/create-group-dialog.component';
import { RemoveGroupDialogComponent } from './remove-group-dialog/remove-group-dialog.component';
import { EditGroupDialogComponent } from './edit-group-dialog/edit-group-dialog.component';
import { catchError, Observable, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { UnauthorizedErrorHandler } from '../../../shared/models/common/401-error-handler/401-error-handler.service';
import { StudentPageViewModel } from '../student-page-viewmodel.service';

@Component({
  selector: 'app-student-groups-list',
  imports: [
    AddIconButtonComponent,
    GreenOutlineButtonComponent,
    StudentGroupCardComponent,
    NgForOf,
    CreateGroupDialogComponent,
    NgIf,
    RemoveGroupDialogComponent,
    EditGroupDialogComponent,
  ],
  templateUrl: './student-groups-list.component.html',
  styleUrl: './student-groups-list.component.scss',
  standalone: true,
})
export class StudentGroupsListComponent implements OnInit {
  public isCreatingNewGroup: boolean = false;
  public removeGroupRequest: StudentGroup | null;
  public editGroupRequest: StudentGroup | null;

  public constructor(
    private readonly _service: StudentGroupsDataService,
    private readonly _handler: UnauthorizedErrorHandler,
    public readonly _viewModel: StudentPageViewModel,
  ) {}

  public ngOnInit() {
    this._service
      .getGroups()
      .pipe(
        tap((response) => {
          this._viewModel.initializeGroups(response);
        }),
        catchError((error: HttpErrorResponse) => {
          this._handler.tryHandle(error);
          return new Observable<never>();
        }),
      )
      .subscribe();
  }

  public handleGroupCreation(group: StudentGroup): void {
    this._viewModel.appendGroup(group);
  }

  public handleGroupSelection(group: StudentGroup): void {
    this._viewModel.setCurrentGroup(group);
  }

  public handleGroupDeletion(group: StudentGroup): void {
    this._viewModel.removeGroup(group);
  }
}
