import { Component, OnInit } from '@angular/core';
import { FacadeService } from '../../services/facade.service';
import { ModalState } from '../../../../../../shared/models/modals/modal-state';

@Component({
  selector: 'app-education-directions-table',
  templateUrl: './education-directions-table.component.html',
  styleUrl: './education-directions-table.component.scss',
})
export class EducationDirectionsTableComponent implements OnInit {
  protected readonly creationModalState: ModalState;
  protected readonly filterModalState: ModalState;
  public constructor(protected readonly facadeService: FacadeService) {
    this.creationModalState = new ModalState();
    this.filterModalState = new ModalState();
  }
  public ngOnInit(): void {
    this.facadeService.fetch();
  }
}
