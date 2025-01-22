import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { NgForOf, NgIf, NgOptimizedImage } from '@angular/common';
import { AddIconButtonComponent } from '../../../building-blocks/buttons/add-icon-button/add-icon-button.component';
import { GreenOutlineButtonComponent } from '../../../building-blocks/buttons/green-outline-button/green-outline-button.component';
import { FloatingLabelInputComponent } from '../../../building-blocks/floating-label-input/floating-label-input.component';
import { EducationDirection } from '../../../modules/administration/submodules/education-directions/models/education-direction-interface';
import { SearchDirectionsService } from './search-directions.service';
import { EducationDirectionItemBlockComponent } from './education-direction-item-block/education-direction-item-block.component';
import { ScrollingModule } from '@angular/cdk/scrolling';
import { CreateEducationDirectionDialogComponent } from './create-education-direction-dialog/create-education-direction-dialog.component';
import { EditEducationDirectionDialogComponent } from './edit-education-direction-dialog/edit-education-direction-dialog.component';
import { DeleteEducationDirectionDialogComponent } from './delete-education-direction-dialog/delete-education-direction-dialog.component';

@Component({
  selector: 'app-education-directions-inline-list',
  imports: [
    NgOptimizedImage,
    AddIconButtonComponent,
    GreenOutlineButtonComponent,
    FloatingLabelInputComponent,
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

  constructor(private readonly _dataService: SearchDirectionsService) {}

  public ngOnInit() {
    this._dataService.getAll().subscribe((response) => {
      this.educationDirections = response;
    });
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
