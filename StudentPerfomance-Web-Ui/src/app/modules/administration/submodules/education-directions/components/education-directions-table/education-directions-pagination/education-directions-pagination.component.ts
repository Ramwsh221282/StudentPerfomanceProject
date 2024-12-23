import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FacadeService } from '../../../services/facade.service';

@Component({
  selector: 'app-education-directions-pagination',
  templateUrl: './education-directions-pagination.component.html',
  styleUrl: './education-directions-pagination.component.scss',
})
export class EducationDirectionsPaginationComponent implements OnInit {
  @Output() pageChange: EventEmitter<void> = new EventEmitter();

  public constructor(protected readonly facadeService: FacadeService) {}

  public ngOnInit(): void {}
}
