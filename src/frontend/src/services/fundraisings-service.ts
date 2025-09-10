import { FundraisingEditDto } from "../models/fundraising-edit-dto";
import { Fundraising, FundraisingType } from "../models/fundraising";
import { CrudService } from "./crud-service";

export class FundraisingsService extends CrudService<Fundraising> {
  constructor() {
    super('fundraisings');
  }

  public byFundId(fundId: number): Promise<Fundraising[]> {
    return this.get('byFund/' + fundId);
  }

  public byFundType(fundType: FundraisingType): Promise<Fundraising[]> {
    return this.get('byFundType/' + fundType);
  }

  public createRegular(fundraising: FundraisingEditDto): Promise<Fundraising> {
    return this.post('regular', fundraising);
  }

  public createPeriodic(fundraising: FundraisingEditDto): Promise<Fundraising> {
    return this.post('periodic', fundraising);
  }

  public createTargeted(fundraising: FundraisingEditDto): Promise<Fundraising> {
    return this.post('targeted', fundraising);
  }

  public copyLastPeriodicalFundraising(fundId: number): Promise<Fundraising> {
    return this.put('copyLastPeriodicalFundraising', null, [['fundId', fundId.toString()]]);
  }
}
