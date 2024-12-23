import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { StudentGroupsFacadeService } from '../../../services/student-groups-facade.service';

@Component({
  selector: 'app-table-pagination',
  templateUrl: './table-pagination.component.html',
  styleUrl: './table-pagination.component.scss',
})
export class TablePaginationComponent implements OnInit {
  @Output() pageChange: EventEmitter<void> = new EventEmitter();

  public constructor(
    protected readonly facadeService: StudentGroupsFacadeService,
  ) {}

  public ngOnInit(): void {
    this.facadeService.refreshPagination();
  }
}
