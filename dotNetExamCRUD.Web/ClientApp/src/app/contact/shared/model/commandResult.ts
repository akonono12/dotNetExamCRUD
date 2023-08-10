export class CommandResult<T> {
  success: boolean = false;
  data: T | undefined;
  errors: string[] = [];
}
