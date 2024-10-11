import { Component, OnInit } from '@angular/core';
import { FacadeService } from '../../services/facade.service';

@Component({
  selector: 'app-education-plans-table',
  templateUrl: './education-plans-table.component.html',
  styleUrl: './education-plans-table.component.scss',
})
export class EducationPlansTableComponent implements OnInit {
  protected creationModalVisibility = false;
  protected filterModalVisibility = false;

  public constructor(protected readonly facadeService: FacadeService) {}

  public ngOnInit(): void {
    this.facadeService.fetch();
  }

  protected startCreation(): void {
    this.creationModalVisibility = true;
  }

  protected stopCreation(value: boolean): void {
    this.creationModalVisibility = value;
  }

  protected startFilter(): void {
    this.filterModalVisibility = true;
  }

  protected stopFilter(value: boolean): void {
    this.filterModalVisibility = value;
  }
}
