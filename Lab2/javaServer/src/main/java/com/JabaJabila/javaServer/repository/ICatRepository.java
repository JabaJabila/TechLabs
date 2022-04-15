package com.JabaJabila.javaServer.repository;

import com.JabaJabila.javaServer.entities.Cat;
import org.springframework.data.repository.CrudRepository;

public interface ICatRepository extends CrudRepository<Cat, Long> {
}