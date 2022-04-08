package com.JabaJabila.javaServer.controllers;

import com.JabaJabila.javaServer.dto.CatCreationInfo;
import com.JabaJabila.javaServer.entities.Cat;
import com.JabaJabila.javaServer.services.CatService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.MediaType;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("cat")
public class CatController {

    @Autowired
    private CatService catService;

    @PostMapping(
            path = "create-cat",
            consumes = {MediaType.APPLICATION_XML_VALUE, MediaType.APPLICATION_JSON_VALUE},
            produces = {MediaType.APPLICATION_XML_VALUE, MediaType.APPLICATION_JSON_VALUE})
    public ResponseEntity<Cat> CreateCat(
            @RequestBody CatCreationInfo catInfo) {
        try {
            Cat cat = catService.createCat(
                    catInfo.getName(), catInfo.getColor(), catInfo.getBreed(), catInfo.getBirthdate());
            return new ResponseEntity(cat, HttpStatus.OK);
        } catch (Exception e) {
            return ResponseEntity.status(HttpStatus.BAD_REQUEST).body(null);
        }
    }

    @GetMapping("/all")
    public ResponseEntity<List<Cat>> getAll() {
        try {
            return new ResponseEntity(catService.getAllCats(), HttpStatus.OK);
        } catch (Exception e) {
            return ResponseEntity.status(HttpStatus.BAD_REQUEST).body(null);
        }
    }

    @GetMapping("/{id}")
    public ResponseEntity<Cat> getCat(@PathVariable Long id) {
        try {
            return new ResponseEntity(catService.findCat(id), HttpStatus.OK);
        } catch (RuntimeException e) {
            return ResponseEntity.status(HttpStatus.NOT_FOUND).body(null);
        }
    }

    @DeleteMapping("delete")
    public HttpStatus deleteCat(@RequestParam Long id) {
        try {
            catService.deleteCat(id);
            return HttpStatus.OK;
        } catch (RuntimeException e) {
            return HttpStatus.NOT_FOUND;
        }
    }
}
