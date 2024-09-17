import { Component, OnInit } from '@angular/core';
import { StudentGroupsFacadeService } from '../../../services/student-groups-facade.service';

@Component({
  selector: 'app-pagination-group',
  templateUrl: './pagination-group.component.html',
  styleUrl: './pagination-group.component.scss',
})
export class PaginationGroupComponent implements OnInit {
  public constructor(
    protected readonly facadeService: StudentGroupsFacadeService
  ) {}

  public ngOnInit(): void {
    this.facadeService.refreshPagination();
  }
}
