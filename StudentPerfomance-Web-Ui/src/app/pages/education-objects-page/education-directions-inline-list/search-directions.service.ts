import { Injectable } from '@angular/core';
import { EducationDirection } from '../../../modules/administration/submodules/education-directions/models/education-direction-interface';
import { Observable } from 'rxjs';
import { BaseHttpService } from '../../../shared/models/common/base-http/base-http.service';

@Injectable({
  providedIn: 'any',
})
export class SearchDirectionsService extends BaseHttpService {
  public getAll(): Observable<EducationDirection[]> {
    const apiUri = `${this._config.baseApiUri}/api/education-direction/all`;
    const headers = this.buildHttpHeaders();
    return this._http.get<EducationDirection[]>(apiUri, {
      headers: headers,
    });
  }
}
