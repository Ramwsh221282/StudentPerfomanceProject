import { Injectable } from '@angular/core';
import { EducationDirection } from '../../../../modules/administration/submodules/education-directions/models/education-direction-interface';
import { DirectionPayloadBuilder } from '../../../../modules/administration/submodules/education-directions/models/contracts/direction-payload-builder';
import { Observable } from 'rxjs';
import { BaseHttpService } from '../../../../shared/models/common/base-http/base-http.service';

@Injectable({
  providedIn: 'any',
})
export class DeleteEducationDirectionService extends BaseHttpService {
  public delete(direction: EducationDirection): Observable<EducationDirection> {
    const url = `${this._config.baseApiUri}/api/education-direction`;
    const body = this.buildPayload(direction);
    const headers = this.buildHttpHeaders();
    return this._http.delete<EducationDirection>(url, {
      headers: headers,
      body,
    });
  }

  private buildPayload(direction: EducationDirection): object {
    return {
      query: DirectionPayloadBuilder(direction),
    };
  }
}
