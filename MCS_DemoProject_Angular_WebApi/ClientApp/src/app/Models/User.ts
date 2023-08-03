export class User {
  public firstName! : string
  public lastName! : string
  public email! : string
  public password! : string
  public  PasswordSalt! : Uint8Array
  public  PasswordHash! :Uint8Array
  public Role!: string
}
