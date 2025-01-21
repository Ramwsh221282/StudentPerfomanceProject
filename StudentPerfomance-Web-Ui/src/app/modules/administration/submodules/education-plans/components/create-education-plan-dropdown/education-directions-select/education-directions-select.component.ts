import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { EducationDirection } from '../../../../education-directions/models/education-direction-interface';
import { SearchDirectionsService } from '../../../../education-directions/services/search-directions.service';
import { DropdownListComponent } from '../../../../../../../building-blocks/dropdown-list/dropdown-list.component';

@Component({
    selector: 'app-education-directions-select',
    imports: [DropdownListComponent],
    templateUrl: './education-directions-select.component.html',
    styleUrl: './education-directions-select.component.scss'
})
export class EducationDirectionsSelectComponent implements OnInit {
  @Input({ required: true }) isSelecting: boolean;
  @Output() itemSelected = new EventEmitter<string>(); // outdated
  @Output() directionSelected = new EventEmitter<EducationDirection>();
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

  protected handleDirectionSelected(info: string): void {
    const parsedData = this.parseData(info);
    const direction = this.directions.find(
      (direction) =>
        direction.name == parsedData.name &&
        direction.code == parsedData.code &&
        direction.type == parsedData.type,
    )!;
    this.directionSelected.emit(direction);
  }

  private parseData(data: string): ParsedDirectionData {
    const nameMatch = data.match(/^\D+/);
    const name = nameMatch![0].trim();
    const remaining = data.substring(name.length).trim();
    const [code, type] = remaining.split(/\s+/);
    return { name, code, type };
  }
}

type ParsedDirectionData = {
  name: string;
  code: string;
  type: string;
};
