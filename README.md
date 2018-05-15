# TACS 2018
## Blockchain 
### Grupo 4

## Recursos Actualizados

#### Registrar un usuario nuevo
POST - /api/user - { username:"...", password:"..." }
> Nota: la primera vez que se alguien se registra es **Admin**, los demas seran usuarios comunes, para volver a registrar otro administrador primero un administrador debe loguearse y con el token ejecutar este recurso:  
**POST - /api/admin - { username:"...", password:"..." }**

#### LogIn
POST - /api/token - { username:"...", password:"..." }

#### LogOut
Token Required!!  
DELETE - /api/token 

#### Compra/Venta
Token Required!!  
POST - /api/user/wallets/{moneda}/transactions - { type: "compra", amount:3 }

#### Ver Portafolios
Token Required!!  
GET - /api/user/wallets

#### Ver Transacciones
Token Required!!  
GET - /api/user/wallets/{moneda}/transactions

#### Ver Informacion de usuarios
Token Required!!  
GET - /api/admin/users/{id}

...

**Basicamente los recursos que tenia el UserId en la ruta, se quitaron ya que la API los atiende con el token y con eso sabe su UserId.**
