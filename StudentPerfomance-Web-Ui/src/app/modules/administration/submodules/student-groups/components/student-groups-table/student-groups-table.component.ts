import { Component, OnInit } from '@angular/core';
import { StudentGroupsFacadeService } from '../../services/student-groups-facade.service';

@Component({
  selector: 'app-student-groups-table',
  templateUrl: './student-groups-table.component.html',
  styleUrl: './student-groups-table.component.scss',
})
export class StudentGroupsTableComponent implements OnInit {
  protected creationModalVisibility: boolean;
  protected filterModalVisibility: boolean;

  public constructor(
    protected readonly facadeService: StudentGroupsFacadeService
  ) {}

  public ngOnInit(): void {
    this.facadeService.fetchData();
  }

  protected openCreationModal(): void {
    this.creationModalVisibility = true;
  }

  protected closeCreationModal(value: boolean): void {
    this.creationModalVisibility = value;
  }

  protected openFilterModal(): void {
    this.filterModalVisibility = true;
  }

  protected closeFilterModal(value: boolean): void {
    this.filterModalVisibility = value;
  }
}
