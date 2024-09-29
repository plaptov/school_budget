import { FundraisingEditDto } from "../models/fundraising-edit-dto";
import { Fundraising, FundraisingType } from "../models/fundraising";
import { CrudService } from "./crud-service";

export class FundraisingsService extends CrudService<Fundraising> {
  constructor() {
    super('fundraisings');
  }

  public byFundId(fundId: number): Promise<Fundraising[]> {
    return this.getRequest('byFundId/' + fundId);
  }

  public byFundType(fundType: FundraisingType): Promise<Fundraising[]> {
    return this.getRequest('byFundType/' + fundType);
  }

  public createRegular(fundraising: FundraisingEditDto): Promise<Fundraising> {
    return this.postRequest('regular', fundraising);
  }

  public createPeriodic(fundraising: FundraisingEditDto): Promise<Fundraising> {
    return this.postRequest('periodic', fundraising);
  }

  public createTargeted(fundraising: FundraisingEditDto): Promise<Fundraising> {
    return this.postRequest('targeted', fundraising);
  }
}
