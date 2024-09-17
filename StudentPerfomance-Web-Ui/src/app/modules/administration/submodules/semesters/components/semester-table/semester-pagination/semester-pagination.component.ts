import { Component, OnInit } from '@angular/core';
import { SemesterFacadeService } from '../../../services/semester-facade.service';

@Component({
  selector: 'app-semester-pagination',
  templateUrl: './semester-pagination.component.html',
  styleUrl: './semester-pagination.component.scss',
})
export class SemesterPaginationComponent implements OnInit {
  public constructor(protected readonly facadeService: SemesterFacadeService) {}

  public ngOnInit(): void {
    this.facadeService.refreshPagination();
  }
}
