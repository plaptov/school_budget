import { FundraisingType } from "./fundraising";

export interface FundraisingEditDto {
  id?: number;
  date: Date;
  name: string;
  description: string;
  recommendedAmount: number;
  fundId?: number;
  type?: FundraisingType;
  isClosed?: boolean;
  closingDate?: Date;
}
