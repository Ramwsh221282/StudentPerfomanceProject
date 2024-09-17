import { Component, OnInit } from '@angular/core';
import { StudentGroupsFacadeService } from '../../services/student-groups-facade.service';

@Component({
  selector: 'app-table-group',
  templateUrl: './table-group.component.html',
  styleUrl: './table-group.component.scss',
})
export class TableGroupComponent implements OnInit {
  public constructor(
    protected readonly facadeService: StudentGroupsFacadeService
  ) {}

  public ngOnInit(): void {
    this.facadeService.fetchData();
  }
}
