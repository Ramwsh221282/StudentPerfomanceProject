import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { DepartmentPaginationService } from '../department-pagination.service';

@Component({
  selector: 'app-department-pagination',
  templateUrl: './department-pagination.component.html',
  styleUrl: './department-pagination.component.scss',
})
export class DepartmentPaginationComponent implements OnInit {
  @Output() pageEmitter: EventEmitter<void> = new EventEmitter();
  public constructor(protected readonly service: DepartmentPaginationService) {}

  ngOnInit(): void {
    this.service.refreshPagination();
  }
}
