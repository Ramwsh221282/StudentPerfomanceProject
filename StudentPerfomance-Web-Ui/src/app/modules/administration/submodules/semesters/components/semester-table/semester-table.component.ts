import { Component, OnInit } from '@angular/core';
import { SemesterFacadeService } from '../../services/semester-facade.service';

@Component({
  selector: 'app-semester-table',
  templateUrl: './semester-table.component.html',
  styleUrl: './semester-table.component.scss',
})
export class SemesterTableComponent implements OnInit {
  public constructor(protected readonly facadeService: SemesterFacadeService) {}

  public ngOnInit(): void {
    this.facadeService.fetch();
  }
}
