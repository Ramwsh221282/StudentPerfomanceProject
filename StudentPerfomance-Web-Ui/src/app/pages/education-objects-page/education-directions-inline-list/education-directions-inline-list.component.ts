import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { NgForOf, NgOptimizedImage } from '@angular/common';
import { AddIconButtonComponent } from '../../../building-blocks/buttons/add-icon-button/add-icon-button.component';
import { GreenOutlineButtonComponent } from '../../../building-blocks/buttons/green-outline-button/green-outline-button.component';
import { FloatingLabelInputComponent } from '../../../building-blocks/floating-label-input/floating-label-input.component';
import { EducationDirection } from '../../../modules/administration/submodules/education-directions/models/education-direction-interface';
import { SearchDirectionsService } from '../../../modules/administration/submodules/education-directions/services/search-directions.service';
import { EducationDirectionItemBlockComponent } from './education-direction-item-block/education-direction-item-block.component';
import { ScrollingModule } from '@angular/cdk/scrolling';

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
  ],
  templateUrl: './education-directions-inline-list.component.html',
  styleUrl: './education-directions-inline-list.component.scss',
  standalone: true,
})
export class EducationDirectionsInlineListComponent implements OnInit {
  public educationDirections: EducationDirection[] = [];
  @Output() selectedEducationDirection: EventEmitter<EducationDirection> =
    new EventEmitter();
  public currentDirection: EducationDirection | null;

  constructor(private readonly _dataService: SearchDirectionsService) {}

  public ngOnInit() {
    this._dataService.getAll().subscribe((response) => {
      this.educationDirections = response;
    });
  }
}
