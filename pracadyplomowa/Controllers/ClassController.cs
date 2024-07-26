using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using pracadyplomowa.Models.DTOs;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Repository.Class;

namespace pracadyplomowa.Controllers
{
    public class ClassController: BaseApiController
    {
        private readonly IClassRepository _classRepository;
        private readonly IMapper _mapper;

        public ClassController(IClassRepository classRepository, IMapper mapper)
        {
            _classRepository = classRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<ClassDTO>> GetClasses()
        {
            var classes = await _classRepository.GetClassList();


            List<ClassDTO> classDTOs = _mapper.Map<List<ClassDTO>>(classes);


            return Ok(classDTOs);
        }

        [HttpPost]
        public async Task<ActionResult<ClassDTO>> PostClass([FromBody] string name){
            Class characterClass = new Class(name);
            for(int i = 0; i < 20; i++){
                characterClass.R_ClassLevels.Add(new ClassLevel(i));
            }
            _classRepository.Add(characterClass);
            await _classRepository.SaveChanges();

            return Ok(new ClassDTO{Id = characterClass.Id, Name = name});
        }
    }
}