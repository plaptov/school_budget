import { BaseEntity } from "./base-entity";

export interface Adult extends BaseEntity {
  name: string;
  phone?: string;
  comment?: string;
}
