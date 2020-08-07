﻿using Controller.Utils;
using Domain.DTO;
using Microsoft.AspNetCore.Mvc;
using Service.Models;
using System;
using System.Linq;

namespace Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : BaseController
    {
        private readonly WalletService _walletService;

        public WalletController(WalletService walletService)
        {
            _walletService = walletService;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            try
            {
                //Pegando o id do usuario pelo token
                long? idUser = GetIdUser();

                if (idUser is null)
                    return NotFound("Usuário não encontrado!");

                var response = _walletService.GetAll(Convert.ToUInt16(idUser));

                return SetResponse(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Save(WalletFormDTO walletDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState.Values.Select(x => x.Errors.Select(x => x.ErrorMessage)));

                //Pegando o id do usuario pelo token
                long? idUser = GetIdUser();

                if (idUser is null)
                    return NotFound("Usuário não encontrado!");

                var response = _walletService.Save(walletDTO, Convert.ToUInt16(idUser));

                return SetResponse(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}