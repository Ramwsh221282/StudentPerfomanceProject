import { Component, OnInit } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { BaseSemesterForm } from '../../models/semester-form-base';
import { SemesterFacadeService } from '../../services/semester-facade.service';

@Component({
  selector: 'app-search-form',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './search-form.component.html',
  styleUrl: './search-form.component.scss',
})
export class SearchFormComponent extends BaseSemesterForm implements OnInit {
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
