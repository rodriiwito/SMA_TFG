# -*- coding: utf-8 -*-
import sys
import os
import pathlib

##########Conexion a la BD##############
import pyodbc as pyodbc
server = 'server' 
database = 'database'
username = 'usuario' 
password = 'password' 

try:
    cnxn = pyodbc.connect('DRIVER={ODBC Driver 17 for SQL Server};SERVER='+server+';DATABASE='+database+';UID='+username+';PWD='+ password+';Connection Timeout =15')
    cursor = cnxn.cursor()
    ##########Conexion a la BD##############

    def recorrerSubdirectorio(ruta):
        with os.scandir(ruta) as itr:
            for entrada in itr:
                if entrada.is_dir():
                    recorrerSubdirectorio(ruta + "/" + entrada.name)
                elif entrada.name[:16] == "Informe-Radiante" and entrada.name[-3:] == "inf":
                    procesaInforme(ruta, entrada.name)

    def procesaInforme(ruta, informe):
        lineasarchivo = []
        with open((ruta + "/" + informe), encoding="utf-8") as fname:
            for lineas in fname:
                lineasarchivo.append(lineas.strip('\n'))
        
        # Apuntamos la línea por la que vamos, para evitar problemas
        actual = 0

        # Ya que vamos a añadir fechas, nos aseguramos de que se suben en formato correspondiente dd/mm/yyyy
        cursor.execute("SET dateformat dmy")

        # Sacamos la lluvia asociada
        idLluviaAsociada = informe[:-4][-4:]
        if idLluviaAsociada[0] == "-":
            idLluviaAsociada = idLluviaAsociada[-3:]
        if idLluviaAsociada == "SPO":
            idLluviaAsociada = "Ninguna"

        # Sacamos la fecha y hora del informe
        yearInforme = informe[17:][:4]
        mesInforme = informe[21:][:2]
        diaInforme = informe[23:][:2]
        horaInforme = informe[25:][:2]
        minutoInforme = informe[27:][:2]
        if(len(informe)>43):
            segundoInforme = informe[29:][:7]
        else:
            segundoInforme = informe[29:][:2] + ".0000"

        fecha = diaInforme + "-" + mesInforme + "-" + yearInforme
        hora = horaInforme + ":" + minutoInforme + ":" + segundoInforme

        # Comprobamos si existe ya un informe en esta hora y dia
        infNuevo = True
        fechaComparar = yearInforme+"-"+mesInforme+"-"+diaInforme
        cursor.execute("SELECT Identificador, Fecha, Hora FROM Informe_Radiante")
        for i in cursor:
            if str(i[1]) == fechaComparar and str(i[2]) == hora:
                infNuevo = False
                idInf = i[0]

        if infNuevo:
            cursor.execute("SELECT COUNT(*) FROM Informe_Radiante")
            for i in cursor:
                idInf = i[0] + 1

        if infNuevo:
            if len(sys.argv) == 1:
                print(ruta + "/" + informe)
            # Comprobamos si existe un meteoro en la base de datos a esta fecha y hora, en caso contrario, lo añadimos
            cursor.execute("SELECT * FROM Meteoro")
            insertar = True
            for i in cursor:
                fechaBien =  str(i[1])[8:][:2] + "-" + str(i[1])[5:][:2] + "-" + str(i[1])[:4]
                if fecha == fechaBien:
                    if hora[:5] ==  i[2][:5]:
                        if float(i[2][6:]) < float(hora[6:]) and float(i[2][6:])+2 > float(hora[6:]):
                            idM = i[0]
                            insertar = False
                        elif float(i[2][6:]) == float(hora[6:]):
                            idM = i[0]
                            insertar = False
                        elif float(i[2][6:]) > float(hora[6:]) and float(i[2][6:])-2 < float(hora[6:]):
                            idM = i[0]
                            insertar = False

            if insertar:
                cursor.execute("SELECT * FROM Meteoro")
                idM = 1
                for i in cursor:
                    idM = i[0] + 1
                insert ="INSERT INTO Meteoro (Identificador, Fecha, Hora) values ("+str(idM)+",'"+fecha+"','"+hora+"')" 
                cursor.execute(insert)
                cursor.commit()

            # Sacamos el número del observatorio
            aux = lineasarchivo[actual].split(' ')
            obsv = aux[len(aux)-1][:2]
            if obsv[1] == ":":
                obsv = obsv[0]
                # Actualizamos actual
            actual = actual + 6

            # Sacamos las lluvias activas a la fecha
            lluvias = []
            while(lineasarchivo[actual] != ""):
                lluvias.append(lineasarchivo[actual])
                actual = actual + 1
                # Actualizamos actual
            actual = actual + 4

            # Sacamos las distancias minimas entre radiantes y trayectoria
            distMinLluvias = []
            while(lineasarchivo[actual] != ""):
                lluviaAct = []
                for i in lineasarchivo[actual].split(' '):
                    if i != "" and i != "|":
                        lluviaAct.append(i)
                distMinLluvias.append(lluviaAct)
                actual = actual+1
                # Actualizamos actual
            actual = actual + 1

            velocidadesAngulares = []
            trayectorias = []

            # Sacamos lluvia asociada al informe (Ya que alguna lluvia es mayor a una palabra, controlamos estas también)
            if lineasarchivo[actual][:2] == "No":
                lluviaAsociada = "Ninguna"

                cursor.execute("SELECT COUNT(*) FROM Informe_Radiante")
                for i in cursor:
                    idInf = i[0] + 1
                insert ="INSERT INTO Informe_Radiante (Identificador, Fecha, Hora, Velocidad_Lluvia_Asociada, Trayectorias_estimadas_para, Distancia_angular_radianes, Distancia_angular_grados, Velocidad_angular_grad_sec, Meteoro_Identificador, Observatorio_Número, Lluvia_Asociada) values ("+str(idInf)+",'"+fecha+"','"+hora+"',NULL,'No medido',NULL,NULL,NULL,"+str(idM)+","+obsv+",'"+idLluviaAsociada+"')" 
                cursor.execute(insert)
                cursor.commit()

                
            else:
                aux = lineasarchivo[actual].split(' ')
                lluviaAsociada = ""
                for i in range(0,len(aux)-11):
                    lluviaAsociada = lluviaAsociada + aux[5 + i] + " "
                # Una vez sacada la lluvia a la que se asocia, sacamos el identificador de la misma
                for i in range(len(lluvias)):
                    if lluvias[i] + " " == lluviaAsociada:
                        lluviaAsociada = distMinLluvias[i][0]
                    # Actualizamos actual
                velLluviaAsociada = aux[len(aux)-2]
                actual = actual + 3

                if lineasarchivo[actual].split(' ')[4] == "tiempo":
                    # Como tenemos lluvia asociada, sacamos las trayectorias estimadas
                    aux = lineasarchivo[actual].split(' ')
                    tiempoTrayectorias = "Un tiempo de " + aux[len(aux)-2]
                        # Actualizamos actual
                    actual = actual + 4

                    # Sacamos las trayectorias
                    while(lineasarchivo[actual] != ""):
                        trayec = []
                        for i in lineasarchivo[actual].split(' '):
                            if i != "" and i != "|":
                                trayec.append(i)
                        trayectorias.append(trayec)
                        actual = actual + 1
                        # Actualizamos actual
                    actual = actual + 1

                    # Sacamos la distancia angular
                    distAngular = []
                    aux = lineasarchivo[actual][62:].split(' ')
                    for i in aux:
                        if i != "" and i !="/":
                            distAngular.append(i)
                        # Actualizamos actual
                    actual = actual + 1

                    # Sacamos la velocidad angular
                    aux = lineasarchivo[actual].split(' ')
                    velAngular = aux[len(aux)-1]
                        # Actualizamos actual
                    actual = actual + 7

                    # Sacamos las velocidades angulares previstas
                    velocidadesAngulares = []
                    while(actual<len(lineasarchivo)):
                        vAng = []
                        aux = lineasarchivo[actual].split(' ')
                        for i in aux:
                            if i != "" and i != "|":
                                vAng.append(i)
                        velocidadesAngulares.append(vAng)
                        actual = actual + 1

                    #Añadimos los datos del informe
                    cursor.execute("SELECT COUNT(*) FROM Informe_Radiante")
                    for i in cursor:
                        idInf = i[0] + 1
                    insert ="INSERT INTO Informe_Radiante (Identificador, Fecha, Hora, Velocidad_Lluvia_Asociada, Trayectorias_estimadas_para, Distancia_angular_radianes, Distancia_angular_grados, Velocidad_angular_grad_sec, Meteoro_Identificador, Observatorio_Número, Lluvia_Asociada) values ("+str(idInf)+",'"+fecha+"','"+hora+"','"+str(velLluviaAsociada)+"','"+str(tiempoTrayectorias)+"',"+str(distAngular[0])+","+str(distAngular[1])+","+str(velAngular)+","+str(idM)+","+obsv+",'"+idLluviaAsociada+"')" 
                    cursor.execute(insert)
                    cursor.commit()

                    for i in trayectorias:
                        insert = "INSERT INTO Trayectoria_estimada (Velocidad, Lon_Inicio, Lat_Inicio, Alt_Inicio, Dist_Inicio, Lon_Final, Lat_Final, Alt_Final, Dist_Final, Recor, e, t, Informe_Radiante_Identificador) values ("+i[0]+",'"+i[1]+"','"+i[2]+"',"+i[3]+","+i[4]+",'"+i[5]+"','"+i[6]+"',"+i[7]+","+i[8]+","+i[9]+",NULL,NULL,"+str(idInf)+")"
                        cursor.execute(insert)
                        cursor.commit()
                
                else:
                    distAngular = "No medido"
                    velAngular = "No medido"
                    # Sacamos el rango de alturas
                    aux = lineasarchivo[actual].split(' ')
                    ranAlt = []
                    ranAlt.append(aux[len(aux)-2])
                    ranAlt.append(aux[len(aux)-1][:2])
                    tiempoTrayectorias = "Rango de alturas " + ranAlt[0] + " " + ranAlt[1]
                    # Actualizamos actual
                    actual = actual + 4


                    # Sacamos las trayectorias
                    while(lineasarchivo[actual] != ""):
                        trayec = []
                        for i in lineasarchivo[actual].split(' '):
                            if i != "" and i != "|":
                                trayec.append(i)
                        trayectorias.append(trayec)
                        actual = actual + 1
                        # Actualizamos actual
                    actual = actual + 4

                    # Sacamos las velocidades angulares
                    while(actual<len(lineasarchivo)):
                        vAng = []
                        aux = lineasarchivo[actual].split(' ')
                        for i in aux:
                            if i != "" and i != "|":
                                vAng.append(i)
                        velocidadesAngulares.append(vAng)
                        actual = actual + 1

                    #Añadimos los datos del informe
                    cursor.execute("SELECT COUNT(*) FROM Informe_Radiante")
                    for i in cursor:
                        idInf = i[0] + 1
                    insert ="INSERT INTO Informe_Radiante (Identificador, Fecha, Hora, Velocidad_Lluvia_Asociada, Trayectorias_estimadas_para, Distancia_angular_radianes, Distancia_angular_grados, Velocidad_angular_grad_sec, Meteoro_Identificador, Observatorio_Número, Lluvia_Asociada) values ("+str(idInf)+",'"+fecha+"','"+hora+"','"+str(velLluviaAsociada)+"','"+str(tiempoTrayectorias)+"',NULL,NULL,NULL,"+str(idM)+","+obsv+",'"+idLluviaAsociada+"')" 
                    cursor.execute(insert)
                    cursor.commit()

                    for i in trayectorias:
                        insert = "INSERT INTO Trayectoria_estimada (Velocidad, Lon_Inicio, Lat_Inicio, Alt_Inicio, Dist_Inicio, Lon_Final, Lat_Final, Alt_Final, Dist_Final, Recor, e, t, Informe_Radiante_Identificador) values (NULL,'"+i[0]+"','"+i[1]+"',"+i[2]+","+i[3]+",'"+i[4]+"','"+i[5]+"',"+i[6]+","+i[7]+",NULL,"+i[8]+","+i[9]+","+str(idInf)+")"
                        cursor.execute(insert)
                        cursor.commit()

            for i in distMinLluvias:
                insert = "INSERT INTO Lluvia_Activa_InfRad (Ar_de_la_fecha, De_de_la_fecha, Ar_más_cercano, De_más_cercano, Distancia, Informe_Radiante_Identificador, Lluvia_Identificador, Lluvia_Año) values ("+i[1]+","+i[2]+","+i[3]+","+i[4]+","+i[5]+","+str(idInf)+",'"+i[0]+"',"+str(yearInforme)+")"
                cursor.execute(insert)
                cursor.commit()

            for i in velocidadesAngulares:
                insert = "INSERT INTO Velociades_Angulares (hi, Lluvia, Meteoro, Informe_Radiante_Identificador) values ("+i[0]+","+i[1]+","+i[2]+","+str(idInf)+")"
                cursor.execute(insert)
                cursor.commit()



    if len(sys.argv) > 1:
        directorio = sys.argv[1]
        with os.scandir(directorio) as itr:
            for entrada in itr:
                if entrada.is_dir():
                    recorrerSubdirectorio(directorio + "/" + entrada.name)
                elif entrada.name[:16] == "Informe-Radiante" and entrada.name[-3:] == "inf":
                    procesaInforme(directorio, entrada.name)
    else:
        directorio = input("Directorio de los informes a cargar: ")
        print("Se cargarán los informes contenidos en el directorio ("+directorio+") y sus subdirectorios")
        sn = input("¿Continuar? (S/N): ")
        if sn == "S":
            with os.scandir(directorio) as itr:
                for entrada in itr:
                    if entrada.is_dir():
                        recorrerSubdirectorio(directorio + "/" + entrada.name)
                    elif entrada.name[:16] == "Informe-Radiante" and entrada.name[-3:] == "inf":
                        procesaInforme(directorio, entrada.name)

        cursor.close()

except pyodbc.OperationalError as e:
    print('Error de conexión:', e)