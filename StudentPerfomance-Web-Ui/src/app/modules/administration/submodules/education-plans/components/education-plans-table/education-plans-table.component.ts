import { Component, OnInit } from '@angular/core';
import { FacadeService } from '../../services/facade.service';
import { ModalState } from '../../../../../../shared/models/modals/modal-state';

@Component({
  selector: 'app-education-plans-table',
  templateUrl: './education-plans-table.component.html',
  styleUrl: './education-plans-table.component.scss',
})
export class EducationPlansTableComponent implements OnInit {
  protected readonly creationModalState: ModalState;
  protected readonly filterByYearModalState: ModalState;
  protected readonly filterByDirectionModalState: ModalState;
  public constructor(protected readonly facadeService: FacadeService) {
    this.creationModalState = new ModalState();
    this.filterByYearModalState = new ModalState();
    this.filterByDirectionModalState = new ModalState();
  }
  public ngOnInit(): void {
    this.facadeService.fetch();
  }
}
