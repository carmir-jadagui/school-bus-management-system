import { ErrorModel } from "./errorModel";

export interface ResultModel<T> {
  data: T;
  ok: boolean;
  message: string;
  errors: ErrorModel[];
}