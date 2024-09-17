import { Component, OnInit } from '@angular/core';
import { BaseSemesterForm } from '../../models/semester-form-base';
import { SemesterFacadeService } from '../../services/semester-facade.service';

@Component({
  selector: 'app-semester-filter',
  templateUrl: './semester-filter.component.html',
  styleUrl: './semester-filter.component.scss',
})
export class SemesterFilterComponent
  extends BaseSemesterForm
  implements OnInit
{
  public constructor(private readonly _facadeService: SemesterFacadeService) {
    super('Поиск семестра');
  }

  public ngOnInit(): void {
    this.initForm();
  }

  protected override submit(): void {
    this.ngOnInit();
  }

  protected searchClick(): void {
    this._facadeService.filter(this.createSemesterFromForm());
    this.submit();
  }

  protected cancelClick(): void {
    this._facadeService.fetch();
    this.submit();
  }
}
