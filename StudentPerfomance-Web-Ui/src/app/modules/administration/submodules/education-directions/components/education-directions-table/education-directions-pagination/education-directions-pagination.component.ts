import { Component, OnInit } from '@angular/core';
import { FacadeService } from '../../../services/facade.service';

@Component({
  selector: 'app-education-directions-pagination',
  templateUrl: './education-directions-pagination.component.html',
  styleUrl: './education-directions-pagination.component.scss',
})
export class EducationDirectionsPaginationComponent implements OnInit {
  public constructor(protected readonly facadeService: FacadeService) {}

  public ngOnInit(): void {
    this.facadeService.refreshPagination();
  }
}
