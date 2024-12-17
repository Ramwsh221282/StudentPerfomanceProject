import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { EducationDirection } from '../../../../education-directions/models/education-direction-interface';
import { SearchDirectionsService } from '../../../../education-directions/services/search-directions.service';
import { DropdownListComponent } from '../../../../../../../building-blocks/dropdown-list/dropdown-list.component';

@Component({
  selector: 'app-education-directions-select',
  standalone: true,
  imports: [DropdownListComponent],
  templateUrl: './education-directions-select.component.html',
  styleUrl: './education-directions-select.component.scss',
})
export class EducationDirectionsSelectComponent implements OnInit {
  @Input({ required: true }) isSelecting: boolean;
  @Output() itemSelected = new EventEmitter<string>();
  @Output() visibilityChanged = new EventEmitter<boolean>();
  protected directions: EducationDirection[] = [];

  public constructor(private readonly _service: SearchDirectionsService) {}

  public ngOnInit(): void {
    this._service.getAll().subscribe((response) => {
      this.directions = response;
    });
  }

  protected getDirectionsData(): string[] {
    const itemsData: string[] = [];
    for (const direction of this.directions) {
      itemsData.push(this.createDirectionDataByMask(direction));
    }
    return itemsData;
  }

  private createDirectionDataByMask(direction: EducationDirection): string {
    return `${direction.name} ${direction.code} ${direction.type}`;
  }
}
